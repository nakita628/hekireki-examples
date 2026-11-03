class ProjectAlert < ApplicationRecord
  self.table_name = "ProjectAlert"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", sent: "SENT", failed: "FAILED" }, default: :pending, validate: true
  enum :type, { task_run: "TASK_RUN", task_run_attempt: "TASK_RUN_ATTEMPT", deployment_failure: "DEPLOYMENT_FAILURE", deployment_success: "DEPLOYMENT_SUCCESS", error_group: "ERROR_GROUP", dashboard_agent_watch: "DASHBOARD_AGENT_WATCH" },
       validate: true

  validates :friendlyId, presence: true, uniqueness: true

  belongs_to :project, foreign_key: "projectId", inverse_of: :alerts
  belongs_to :environment, class_name: "RuntimeEnvironment", foreign_key: "environmentId", inverse_of: :alerts
  belongs_to :channel, class_name: "ProjectAlertChannel", foreign_key: "channelId", inverse_of: :alerts
  belongs_to :worker_deployment, foreign_key: "workerDeploymentId", optional: true, inverse_of: :alerts
end
