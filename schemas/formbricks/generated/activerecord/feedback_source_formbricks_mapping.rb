class FeedbackSourceFormbricksMapping < ApplicationRecord
  self.table_name = "FeedbackSourceFormbricksMapping"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :hubFieldType, { text: "text", categorical: "categorical", nps: "nps", csat: "csat", ces: "ces", rating: "rating", number: "number", boolean: "boolean", date: "date" },
       validate: true

  validates :elementId, presence: true, uniqueness: { scope: [ :workspaceId, :feedback_source_id, :surveyId ] }

  belongs_to :feedback_source, foreign_key: [ :feedback_source_id, :workspaceId ], primary_key: [ :id, :workspaceId ],
                               inverse_of: :formbricks_mappings
  belongs_to :survey, foreign_key: [ :surveyId, :workspaceId ], primary_key: [ :id, :workspaceId ],
                      inverse_of: :feedback_source_mappings
end
