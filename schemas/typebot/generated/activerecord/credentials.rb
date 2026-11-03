class Credentials < ApplicationRecord
  self.table_name = "Credentials"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :data, presence: true
  validates :name, presence: true
  validates :type, presence: true
  validates :iv, presence: true

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :credentials
end
