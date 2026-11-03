class ViewerGroupMembership < ApplicationRecord
  self.table_name = "ViewerGroupMembership"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :viewerId, uniqueness: { scope: :groupId }

  belongs_to :viewer, foreign_key: "viewerId", inverse_of: :groups
  belongs_to :group, class_name: "ViewerGroup", foreign_key: "groupId", inverse_of: :members
end
