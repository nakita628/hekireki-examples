class Reaction < ApplicationRecord
  self.table_name = "Reaction"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :pageNumber, presence: true
  validates :type, presence: true

  belongs_to :view, foreign_key: "viewId", inverse_of: :reactions
end
