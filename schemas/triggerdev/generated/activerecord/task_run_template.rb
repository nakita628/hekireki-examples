class TaskRunTemplate < ApplicationRecord
  self.table_name = "TaskRunTemplate"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :payloadType, default: "application/json"
  attribute :metadataType, default: "application/json"
  attribute :tags, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :triggerSource, { standard: "STANDARD", scheduled: "SCHEDULED", agent: "AGENT", webhook: "WEBHOOK" },
       validate: true

  validates :taskSlug, presence: true
  validates :label, presence: true
  validates :projectId, presence: true
  validates :organizationId, presence: true
end
