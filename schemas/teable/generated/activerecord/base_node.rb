class BaseNode < ApplicationRecord
  self.table_name = "base_node"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :base_id, presence: true, uniqueness: { scope: %i[resource_type resource_id] }
  validates :resource_type, presence: true
  validates :resource_id, presence: true
  validates :order, presence: true
  validates :created_by, presence: true

  belongs_to :parent, class_name: "BaseNode", optional: true, inverse_of: :children
  has_many :children, class_name: "BaseNode", foreign_key: "parent_id", inverse_of: :parent, dependent: :nullify
end
