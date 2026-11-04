class WorkspaceDocuments < ApplicationRecord
  attribute :pinned, default: false
  attribute :watched, default: false
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :docId, presence: true, uniqueness: true
  validates :filename, presence: true
  validates :docpath, presence: true

  belongs_to :workspace, class_name: "Workspaces", foreign_key: "workspaceId", inverse_of: :documents
  has_one :document_sync_queues, foreign_key: "workspaceDocId", inverse_of: :workspace_doc, dependent: :destroy
end
