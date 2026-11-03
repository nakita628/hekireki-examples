class Feedback < ApplicationRecord
  self.table_name = "Feedback"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :linkId, uniqueness: true
  validates :data, exclusion: { in: [ nil ] }

  belongs_to :link, foreign_key: "linkId", inverse_of: :feedback
  has_many :responses, class_name: "FeedbackResponse", foreign_key: "feedbackId", inverse_of: :feedback,
                       dependent: :destroy
end
