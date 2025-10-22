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
    		bat '''
    		dotnet test --configuration Release --logger "trx;LogFileName=test_results.trx"
    		dotnet tool install -g trx2junit
    		set PATH=%PATH%;%USERPROFILE%\\.dotnet\\tools
    		trx2junit "BankingApplicationTests\\TestResults\\test_results.trx"
    		'''
	}
    }

    post {
        always {
            junit '**/test_results.xml'
        }
        failure {
            echo '❌ Build or tests failed!'
        }
        success {
            echo '✅ Build and tests passed!'
        }
    }
}