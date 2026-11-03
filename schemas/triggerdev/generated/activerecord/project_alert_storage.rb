class ProjectAlertStorage < ApplicationRecord
  self.table_name = "ProjectAlertStorage"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :alertType, { task_run: "TASK_RUN", task_run_attempt: "TASK_RUN_ATTEMPT", deployment_failure: "DEPLOYMENT_FAILURE", deployment_success: "DEPLOYMENT_SUCCESS", error_group: "ERROR_GROUP", dashboard_agent_watch: "DASHBOARD_AGENT_WATCH" },
       validate: true

  validates :storageId, presence: true
  validates :storageData, exclusion: { in: [ nil ] }

  belongs_to :project, foreign_key: "projectId", inverse_of: :alert_storages
  belongs_to :alert_channel, class_name: "ProjectAlertChannel", foreign_key: "alertChannelId",
                             inverse_of: :alert_storages
end
