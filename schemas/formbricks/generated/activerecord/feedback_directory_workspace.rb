class FeedbackDirectoryWorkspace < ApplicationRecord
  self.table_name = "FeedbackDirectoryWorkspace"
  self.primary_key = %w[feedbackDirectoryId workspaceId]

  belongs_to :feedback_directory, foreign_key: "feedbackDirectoryId", inverse_of: :workspaces
  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :feedback_directory_workspaces
  has_many :feedback_sources, foreign_key: [ :feedbackDirectoryId, :workspaceId ],
                              inverse_of: :feedback_directory_workspace, dependent: :restrict_with_error
end
