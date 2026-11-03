class LinkVisitorGroup < ApplicationRecord
  self.table_name = "LinkVisitorGroup"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :linkId, uniqueness: { scope: :visitorGroupId }

  belongs_to :link, foreign_key: "linkId", inverse_of: :visitor_groups
  belongs_to :visitor_group, foreign_key: "visitorGroupId", inverse_of: :links
end
