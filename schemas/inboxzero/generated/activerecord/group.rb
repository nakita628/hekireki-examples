class Group < ApplicationRecord
  self.table_name = "Group"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :emailAccountId }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :groups
  has_one :rule, foreign_key: "groupId", inverse_of: :group, dependent: :destroy
  has_many :items, class_name: "GroupItem", foreign_key: "groupId", inverse_of: :group, dependent: :destroy
end
