class Workspaces < ApplicationRecord
  attribute :createdAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :openAiHistory, default: 20
  attribute :lastUpdatedAt, PrismaDateTime.new, default: -> { Time.current }
  attribute :similarityThreshold, default: 0.25
  attribute :topN, default: 4
  attribute :chatMode, default: "chat"
  attribute :vectorSearchMode, default: "default"
  alias_attribute :created_at, :createdAt

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: true

  has_many :workspace_users, class_name: "WorkspaceUsers", foreign_key: "workspace_id", inverse_of: :workspaces,
                             dependent: :destroy
  has_many :documents, class_name: "WorkspaceDocuments", foreign_key: "workspaceId", inverse_of: :workspace,
                       dependent: :restrict_with_error
  has_many :workspace_suggested_messages, class_name: "WorkspaceSuggestedMessages", foreign_key: "workspaceId",
                                          inverse_of: :workspace, dependent: :destroy
  has_many :embed_configs, class_name: "EmbedConfigs", foreign_key: "workspace_id", inverse_of: :workspace,
                           dependent: :destroy
  has_many :threads, class_name: "WorkspaceThreads", foreign_key: "workspace_id", inverse_of: :workspace,
                     dependent: :destroy
  has_many :workspace_agent_invocations, class_name: "WorkspaceAgentInvocations", foreign_key: "workspace_id",
                                         inverse_of: :workspace, dependent: :destroy
  has_many :prompt_history, foreign_key: "workspaceId", inverse_of: :workspace, dependent: :destroy
  has_many :workspace_parsed_files, class_name: "WorkspaceParsedFiles", foreign_key: "workspaceId",
                                    inverse_of: :workspace, dependent: :destroy
  has_many :memories, class_name: "Memories", foreign_key: "workspace_id", inverse_of: :workspace, dependent: :destroy
end
