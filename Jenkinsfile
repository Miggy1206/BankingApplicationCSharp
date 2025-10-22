pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore Dependencies') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test --configuration Release --logger "trx;LogFileName=test_results.trx"'
            }
        }
    }

    post {
        always {
            junit '**/TestResults/*.xml'
        }
        failure {
            echo '❌ Build or tests failed!'
        }
        success {
            echo '✅ Build and tests passed!'
        }
    }
}