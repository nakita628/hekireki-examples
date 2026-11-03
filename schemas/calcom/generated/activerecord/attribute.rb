class Attribute < ApplicationRecord
  self.table_name = "Attribute"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :enabled, default: true
  attribute :usersCanEditRelation, default: false
  attribute :isWeightsEnabled, default: false
  attribute :isLocked, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :type, { text: "TEXT", number: "NUMBER", single_select: "SINGLE_SELECT", multi_select: "MULTI_SELECT" },
       validate: true

  validates :name, presence: true
  validates :slug, presence: true, uniqueness: { scope: :teamId }

  belongs_to :team, foreign_key: "teamId", inverse_of: :team_attributes
  has_many :options, class_name: "AttributeOption", foreign_key: "attributeId", inverse_of: :attribute_option_attribute,
                     dependent: :destroy
  has_many :attribute_sync_field_mappings, foreign_key: "attributeId",
                                           inverse_of: :attribute_sync_field_mapping_attribute, dependent: :destroy
end
