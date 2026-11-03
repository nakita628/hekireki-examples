class VisitorGroup < ApplicationRecord
  self.table_name = "VisitorGroup"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :emails, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true

  belongs_to :team, foreign_key: "teamId", inverse_of: :visitor_groups
  has_many :links, class_name: "LinkVisitorGroup", foreign_key: "visitorGroupId", inverse_of: :visitor_group,
                   dependent: :destroy
end
