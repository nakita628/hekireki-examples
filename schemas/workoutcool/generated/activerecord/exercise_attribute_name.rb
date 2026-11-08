class ExerciseAttributeName < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :name, { type: "TYPE", primary_muscle: "PRIMARY_MUSCLE", secondary_muscle: "SECONDARY_MUSCLE", equipment: "EQUIPMENT", mechanics_type: "MECHANICS_TYPE" },
       validate: true

  has_many :values, class_name: "ExerciseAttributeValue", foreign_key: "attributeNameId", inverse_of: :attribute_name,
                    dependent: :restrict_with_error
  has_many :exercise_attributes, foreign_key: "attributeNameId", inverse_of: :attribute_name,
                                 dependent: :restrict_with_error
end
