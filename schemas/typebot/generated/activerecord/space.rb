class Space < ApplicationRecord
  self.table_name = "Space"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :id, uniqueness: { scope: :workspaceId }
  validates :name, presence: true

  belongs_to :workspace, foreign_key: "workspaceId", inverse_of: :spaces
  has_many :typebots, foreign_key: "spaceId", inverse_of: :space, dependent: :nullify
end
