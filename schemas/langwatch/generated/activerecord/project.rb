class Project < ApplicationRecord
  self.table_name = "Project"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :kind, default: "application"
  attribute :firstMessage, default: false
  attribute :integrated, default: false
  attribute :traceSharingEnabled, default: true
  attribute :presenceEnabled, default: true
  attribute :isPersonal, default: false
  attribute :personalFeatures, default: -> { JSON.parse("{}") }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: true
  validates :apiKey, presence: true, uniqueness: true
  validates :lwqlKey, uniqueness: true
  validates :language, presence: true
  validates :framework, presence: true
  validates :ownerUserId, length: { maximum: 255 }

  belongs_to :team, foreign_key: "teamId", inverse_of: :projects
  belongs_to :owner_user, class_name: "User", foreign_key: "ownerUserId", optional: true, inverse_of: :personal_projects
  has_many :checks, class_name: "MonitorRecord", foreign_key: "projectId", inverse_of: :project,
                    dependent: :restrict_with_error
  has_many :costs, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :topics, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :datasets, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :dataset_records, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :custom_graphs, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :dashboards, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :batch_evaluations, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :triggers, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :experiments, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :annotations, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :project_secrets, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :trigger_sent, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :webhook_deliveries, class_name: "WebhookEndpointDelivery", foreign_key: "projectId", inverse_of: :project,
                                dependent: :destroy
  has_many :annotation_scores, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :share_links, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :workflows, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :workflow_version, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :annotation_queue, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :annotation_queue_item, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :llm_prompt_configs, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :analytics, class_name: "Analytics", foreign_key: "projectId", inverse_of: :project,
                       dependent: :restrict_with_error
  has_many :notifications, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :agents, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :evaluators, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :scenarios, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :suite_configurations, class_name: "SimulationSuite", foreign_key: "projectId", inverse_of: :project,
                                  dependent: :restrict_with_error
  has_many :saved_views, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :gateway_guardrails, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :gateway_change_events, foreign_key: "projectId", inverse_of: :project, dependent: :nullify
  has_many :pinned_traces, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :trace_edit_overlays, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
  has_many :email_suppressions, foreign_key: "projectId", inverse_of: :project, dependent: :restrict_with_error
end
