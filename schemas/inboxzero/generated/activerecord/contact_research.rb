class ContactResearch < ApplicationRecord
  self.table_name = "ContactResearch"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :sources, default: -> { [] }
  alias_attribute :created_at, :createdAt

  validates :email, presence: true
  validates :researchStartedAt, presence: true
  validates :found, inclusion: { in: [ true, false ] }
end
