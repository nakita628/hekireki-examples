class FeedbackSource < ApplicationRecord
  self.table_name = "FeedbackSource"
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :type, { formbricks_survey: "formbricks_survey", csv: "csv" }, validate: true
  enum :status, { active: "active", paused: "paused", error: "error" }, default: :active, validate: true
  enum :import_mode, { completed_only: "completedOnly", all: "all" }, default: :completed_only, prefix: true,
                                                                      validate: true
  enum :elementScope, { all: "all", specific: "specific" }, default: :specific, prefix: true, validate: true

  validates :id, uniqueness: { scope: :workspaceId }
  validates :name, presence: true, uniqueness: { scope: :workspaceId }

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :feedback_sources
  belongs_to :feedback_directory, foreign_key: "feedbackDirectoryId", inverse_of: :feedback_sources
  belongs_to :creator, class_name: "User", foreign_key: "created_by", optional: true, inverse_of: :feedback_sources
  belongs_to :feedback_directory_workspace, foreign_key: [ :feedbackDirectoryId, :workspaceId ],
                                            inverse_of: :feedback_sources
  has_many :formbricks_mappings, class_name: "FeedbackSourceFormbricksMapping",
                                 foreign_key: [ :feedback_source_id, :workspaceId ], primary_key: [ :id, :workspaceId ],
                                 inverse_of: :feedback_source, dependent: :destroy
  has_many :field_mappings, class_name: "FeedbackSourceFieldMapping", foreign_key: [ :feedback_source_id, :workspaceId ],
                            primary_key: [ :id, :workspaceId ], inverse_of: :feedback_source, dependent: :destroy
end
