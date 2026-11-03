class MonitorRecord < ApplicationRecord
  self.table_name = "monitors"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :tags, default: -> { [] }
  attribute :trigger_ids, default: -> { [] }

  enum :view, { observations: "OBSERVATIONS", scores_numeric: "SCORES_NUMERIC", scores_categorical: "SCORES_CATEGORICAL", scores_boolean: "SCORES_BOOLEAN" },
       validate: true
  enum :threshold_operator, { gt: "GT", gte: "GTE", lt: "LT", lte: "LTE", eq: "EQ", neq: "NEQ" }, validate: true
  enum :severity, { paused: "PAUSED", unknown: "UNKNOWN", no_data: "NO_DATA", ok: "OK", warning: "WARNING", alert: "ALERT" },
       default: :unknown, validate: true
  enum :status, { paused: "PAUSED", active: "ACTIVE", error_bad_query: "ERROR_BAD_QUERY" }, default: :active,
                                                                                            prefix: true, validate: true

  validates :filters, exclusion: { in: [ nil ] }
  validates :metric, exclusion: { in: [ nil ] }
  validates :window_ms, presence: true
  validates :cadence_ms, presence: true
  validates :alert_threshold, presence: true
  validates :no_data, exclusion: { in: [ nil ] }
  validates :renotify, exclusion: { in: [ nil ] }
  validates :scheduler_batch_id, presence: true
  validates :name, presence: true

  belongs_to :created_by_user, class_name: "User", foreign_key: "created_by", optional: true,
                               inverse_of: :monitors_created
  belongs_to :updated_by_user, class_name: "User", foreign_key: "updated_by", optional: true,
                               inverse_of: :monitors_updated
  belongs_to :project, inverse_of: :monitors
end
