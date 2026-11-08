class ExerciseAttribute < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :exerciseId, uniqueness: { scope: [ :attributeNameId, :attributeValueId ] }

  belongs_to :exercise, foreign_key: "exerciseId", inverse_of: :exercise_attributes
  belongs_to :attribute_name, class_name: "ExerciseAttributeName", foreign_key: "attributeNameId",
                              inverse_of: :exercise_attributes
  belongs_to :attribute_value, class_name: "ExerciseAttributeValue", foreign_key: "attributeValueId",
                               inverse_of: :exercise_attributes
end
