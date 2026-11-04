class WorkspaceThreads < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: true

  belongs_to :workspace, class_name: "Workspaces", inverse_of: :threads
  belongs_to :user, class_name: "Users", optional: true, inverse_of: :threads
  has_many :workspace_parsed_files, class_name: "WorkspaceParsedFiles", foreign_key: "threadId", inverse_of: :thread,
                                    dependent: :destroy
end
