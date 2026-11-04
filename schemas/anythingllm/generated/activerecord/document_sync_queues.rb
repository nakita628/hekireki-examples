class DocumentSyncQueues < ApplicationRecord
  attribute :staleAfterMs, default: 604_800_000
  attribute :nextSyncAt, PrismaDateTime.new
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastSyncedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :nextSyncAt, presence: true
  validates :workspaceDocId, uniqueness: true

  belongs_to :workspace_doc, class_name: "WorkspaceDocuments", foreign_key: "workspaceDocId", optional: true,
                             inverse_of: :document_sync_queues
  has_many :runs, class_name: "DocumentSyncExecutions", foreign_key: "queueId", inverse_of: :queue, dependent: :destroy
end
