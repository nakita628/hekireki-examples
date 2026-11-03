class RuntimeEnvironment < ApplicationRecord
  self.table_name = "RuntimeEnvironment"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isBranchableEnvironment, default: false
  attribute :previewAutoArchiveExcludedBranches, default: -> { [] }
  attribute :maximumConcurrencyLimit, default: 5
  attribute :concurrencyLimitBurstFactor, default: BigDecimal("2")
  attribute :paused, default: false
  attribute :autoEnableInternalSources, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { production: "PRODUCTION", staging: "STAGING", development: "DEVELOPMENT", preview: "PREVIEW" },
       default: :development, validate: true
  enum :pauseSource, { billing_limit: "BILLING_LIMIT" }, validate: { allow_nil: true }

  validates :slug, presence: true,
                   uniqueness: { scope: [ :projectId, :orgMemberId ], conditions: -> { where.not(orgMemberId: nil) } }
  validates :apiKey, presence: true, uniqueness: true
  validates :pkApiKey, presence: true, uniqueness: true
  validates :shortcode, presence: true, uniqueness: { scope: :projectId }

  belongs_to :parent_environment, class_name: "RuntimeEnvironment", foreign_key: "parentEnvironmentId", optional: true,
                                  inverse_of: :child_environments
  belongs_to :organization, foreign_key: "organizationId", inverse_of: :environments
  belongs_to :project, foreign_key: "projectId", inverse_of: :environments
  belongs_to :org_member, foreign_key: "orgMemberId", optional: true, inverse_of: :environments
  belongs_to :current_session, class_name: "RuntimeEnvironmentSession", foreign_key: "currentSessionId", optional: true,
                               inverse_of: :current_environments
  has_many :child_environments, class_name: "RuntimeEnvironment", foreign_key: "parentEnvironmentId",
                                inverse_of: :parent_environment, dependent: :destroy
  has_many :background_workers, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment,
                                dependent: :destroy
  has_many :background_worker_tasks, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment,
                                     dependent: :destroy
  has_many :task_runs, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment, dependent: :destroy
  has_many :task_queues, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment, dependent: :destroy
  has_many :environment_variable_values, foreign_key: "environmentId", inverse_of: :environment, dependent: :destroy
  has_many :checkpoints, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment, dependent: :destroy
  has_many :worker_deployments, foreign_key: "environmentId", inverse_of: :environment, dependent: :destroy
  has_many :worker_deployment_promotions, foreign_key: "environmentId", inverse_of: :environment, dependent: :destroy
  has_many :task_run_attempts, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment,
                               dependent: :destroy
  has_many :checkpoint_restore_event, class_name: "CheckpointRestoreEvent", foreign_key: "runtimeEnvironmentId",
                                      inverse_of: :runtime_environment, dependent: :destroy
  has_many :task_schedule_instances, foreign_key: "environmentId", inverse_of: :environment, dependent: :destroy
  has_many :alerts, class_name: "ProjectAlert", foreign_key: "environmentId", inverse_of: :environment,
                    dependent: :destroy
  has_many :sessions, class_name: "RuntimeEnvironmentSession", foreign_key: "environmentId", inverse_of: :environment,
                      dependent: :destroy
  has_many :task_run_number_counter, class_name: "TaskRunNumberCounter", foreign_key: "environmentId",
                                     inverse_of: :environment, dependent: :destroy
  has_many :task_run_checkpoints, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment,
                                  dependent: :destroy
  has_many :waitpoints, foreign_key: "environmentId", inverse_of: :environment, dependent: :destroy
  has_many :worker_instances, foreign_key: "environmentId", inverse_of: :environment, dependent: :destroy
  has_many :waitpoint_tags, foreign_key: "environmentId", inverse_of: :environment, dependent: :destroy
  has_many :bulk_action_group, class_name: "BulkActionGroup", foreign_key: "environmentId", inverse_of: :environment,
                               dependent: :destroy
  has_many :customer_queries, foreign_key: "environmentId", inverse_of: :environment, dependent: :destroy
  has_many :prompts, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment, dependent: :destroy
  has_many :playground_conversations, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment,
                                      dependent: :destroy
  has_many :error_group_states, foreign_key: "environmentId", inverse_of: :environment, dependent: :destroy
  has_many :task_identifiers, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment, dependent: :destroy
  has_many :api_keys, dependent: :destroy
  has_many :revoked_api_keys, foreign_key: "runtimeEnvironmentId", inverse_of: :runtime_environment, dependent: :destroy
end
