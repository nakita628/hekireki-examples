class TaskEvent < ApplicationRecord
  self.table_name = "TaskEvent"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isError, default: false
  attribute :isPartial, default: false
  attribute :isCancelled, default: false
  attribute :isDebug, default: false
  attribute :duration, default: 0
  attribute :runIsTest, default: false
  attribute :usageDurationMs, default: 0
  attribute :usageCostInCents, default: 0
  alias_attribute :created_at, :createdAt

  enum :level, { trace: "TRACE", debug: "DEBUG", log: "LOG", info: "INFO", warn: "WARN", error: "ERROR" },
       default: :trace, prefix: true, validate: true
  enum :kind, { unspecified: "UNSPECIFIED", internal: "INTERNAL", server: "SERVER", client: "CLIENT", producer: "PRODUCER", consumer: "CONSUMER", unrecognized: "UNRECOGNIZED", log: "LOG" },
       default: :internal, prefix: true, validate: true
  enum :status, { unset: "UNSET", ok: "OK", error: "ERROR", unrecognized: "UNRECOGNIZED" }, default: :unset,
                                                                                            prefix: true, validate: true
  enum :environmentType, { production: "PRODUCTION", staging: "STAGING", development: "DEVELOPMENT", preview: "PREVIEW" },
       validate: true

  validates :message, presence: true
  validates :traceId, presence: true
  validates :spanId, presence: true
  validates :serviceName, presence: true
  validates :serviceNamespace, presence: true
  validates :startTime, presence: true
  validates :environmentId, presence: true
  validates :organizationId, presence: true
  validates :projectId, presence: true
  validates :projectRef, presence: true
  validates :runId, presence: true
  validates :taskSlug, presence: true
  validates :properties, exclusion: { in: [ nil ] }
end
