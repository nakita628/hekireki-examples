class FeedbackSourceFieldMapping < ApplicationRecord
  self.table_name = "FeedbackSourceFieldMapping"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :source_field_id, presence: true,
                              uniqueness: { scope: [ :workspaceId, :feedback_source_id, :target_field_id ] }
  validates :target_field_id, presence: true

  belongs_to :feedback_source, foreign_key: [ :feedback_source_id, :workspaceId ], primary_key: [ :id, :workspaceId ],
                               inverse_of: :field_mappings
end
