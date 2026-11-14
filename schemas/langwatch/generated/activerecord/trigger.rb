class Trigger < ApplicationRecord
  self.table_name = "Trigger"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :lastRunAt, default: 0
  attribute :active, default: true
  attribute :deleted, default: false
  attribute :notificationCadence, default: "immediate"
  attribute :traceDebounceMs, default: 30_000
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :action, { send_email: "SEND_EMAIL", add_to_dataset: "ADD_TO_DATASET", add_to_annotation_queue: "ADD_TO_ANNOTATION_QUEUE", send_slack_message: "SEND_SLACK_MESSAGE", send_webhook: "SEND_WEBHOOK" },
       validate: true
  enum :triggerKind, { automation: "AUTOMATION", alert: "ALERT", report: "REPORT" }, default: :automation,
                                                                                     validate: true
  enum :alertType, { critical: "CRITICAL", warning: "WARNING", info: "INFO" }, validate: { allow_nil: true }

  validates :name, presence: true
  validates :actionParams, exclusion: { in: [ nil ] }
  validates :filters, exclusion: { in: [ nil ] }
  validates :customGraphId, uniqueness: { allow_nil: true }

  belongs_to :project, foreign_key: "projectId", inverse_of: :triggers
  belongs_to :custom_graph, foreign_key: "customGraphId", optional: true, inverse_of: :trigger
  has_one :latest_evaluation, class_name: "TriggerLatestEvaluation", foreign_key: "triggerId", inverse_of: :trigger,
                              dependent: :destroy
  has_many :trigger_sent, foreign_key: "triggerId", inverse_of: :trigger, dependent: :restrict_with_error
  has_many :webhook_deliveries, class_name: "WebhookEndpointDelivery", foreign_key: "triggerId", inverse_of: :trigger,
                                dependent: :destroy
end
