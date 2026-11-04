class DocumentSyncExecutions < ApplicationRecord
  attribute :status, default: "unknown"
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  belongs_to :queue, class_name: "DocumentSyncQueues", foreign_key: "queueId", inverse_of: :runs
end
