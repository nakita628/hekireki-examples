class WorkspaceParsedFiles < ApplicationRecord
  attribute :tokenCountEstimate, default: 0
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :filename, presence: true, uniqueness: true

  belongs_to :workspace, class_name: "Workspaces", foreign_key: "workspaceId", inverse_of: :workspace_parsed_files
  belongs_to :user, class_name: "Users", foreign_key: "userId", optional: true, inverse_of: :workspace_parsed_files
  belongs_to :thread, class_name: "WorkspaceThreads", foreign_key: "threadId", optional: true,
                      inverse_of: :workspace_parsed_files
end
