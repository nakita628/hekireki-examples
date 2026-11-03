class SurveyAttributeFilter < ApplicationRecord
  self.table_name = "SurveyAttributeFilter"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  enum :condition, { equals: "equals", not_equals: "notEquals" }, validate: true

  validates :surveyId, uniqueness: { scope: :attributeKeyId }
  validates :value, presence: true

  belongs_to :attribute_key, class_name: "ContactAttributeKey", foreign_key: "attributeKeyId",
                             inverse_of: :attribute_filters
  belongs_to :survey, foreign_key: "surveyId", inverse_of: :attribute_filters
end
