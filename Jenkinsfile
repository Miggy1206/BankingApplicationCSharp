pipeline {
    agent any

    triggers {
        // Trigger automatically on git push (requires webhook or polling)
        githubPush()
    }


    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore Dependencies') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --no-restore'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test --no-build --logger "trx;LogFileName=test_results.trx"'
            }

            post {
                always {
                    // Publish xUnit results (convert .trx to .xml first)
                    xunit (
                        tools: [MSTest(deleteOutputFiles: true, pattern: '**/test_results.trx', skipNoTestFiles: false, stopProcessingIfError: false)]
                    )
                }
            }
        }
    }

    post {
        success {
            echo '✅ Build and tests succeeded!'
        }
        failure {
            echo '❌ Build or tests failed!'
        }
    }
}