class AttributeOption < ApplicationRecord
  self.table_name = "AttributeOption"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :isGroup, default: false
  attribute :contains, default: -> { [] }

  validates :value, presence: true
  validates :slug, presence: true

  belongs_to :attribute, foreign_key: "attributeId", inverse_of: :options
  has_many :assigned_users, class_name: "AttributeToUser", foreign_key: "attributeOptionId",
                            inverse_of: :attribute_option, dependent: :destroy
end
