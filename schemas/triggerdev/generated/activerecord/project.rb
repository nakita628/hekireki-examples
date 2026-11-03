class Project < ApplicationRecord
  self.table_name = "Project"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :allowedMasterQueues, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :version, { v2: "V2", v3: "V3" }, default: :v2, validate: true
  enum :engine, { v1: "V1", v2: "V2" }, default: :v1, prefix: true, validate: true

  validates :slug, presence: true, uniqueness: true
  validates :name, presence: true
  validates :externalRef, presence: true, uniqueness: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :projects
  belongs_to :default_worker_group, class_name: "WorkerInstanceGroup", foreign_key: "defaultWorkerGroupId",
                                    optional: true, inverse_of: :default_for_projects
  has_one :connected_github_repository, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :worker_groups, class_name: "WorkerInstanceGroup", foreign_key: "projectId", inverse_of: :project,
                           dependent: :destroy
  has_many :workers, class_name: "WorkerInstance", foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :environments, class_name: "RuntimeEnvironment", foreign_key: "projectId", inverse_of: :project,
                          dependent: :destroy
  has_many :background_workers, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :background_worker_tasks, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :task_runs, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :run_tags, class_name: "TaskRunTag", foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :task_queues, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :environment_variables, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :checkpoints, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :worker_deployment, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :checkpoint_restore_event, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :task_schedules, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :alert_channels, class_name: "ProjectAlertChannel", foreign_key: "projectId", inverse_of: :project,
                            dependent: :destroy
  has_many :alerts, class_name: "ProjectAlert", foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :alert_storages, class_name: "ProjectAlertStorage", foreign_key: "projectId", inverse_of: :project,
                            dependent: :destroy
  has_many :bulk_action_groups, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :background_worker_file, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :waitpoints, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :task_run_waitpoints, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :task_run_checkpoints, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :waitpoint_tags, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :organization_project_integration, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :customer_queries, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :playground_conversations, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :task_schedule_instances, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :metrics_dashboards, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :llm_models, dependent: :destroy
  has_many :prompts, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :platform_notifications, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :error_group_states, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :task_identifiers, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
end
