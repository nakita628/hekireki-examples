class Tag < ApplicationRecord
  self.table_name = "Tag"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :teamId }
  validates :color, presence: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :tag
  has_many :items, class_name: "TagItem", foreign_key: "tagId", inverse_of: :tag, dependent: :destroy
end
