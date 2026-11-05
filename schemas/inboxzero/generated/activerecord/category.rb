class Category < ApplicationRecord
  self.table_name = "Category"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true, uniqueness: { scope: :emailAccountId }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :categories
  has_many :email_senders, class_name: "Newsletter", foreign_key: "categoryId", inverse_of: :category,
                           dependent: :nullify
  has_and_belongs_to_many :rules, join_table: "_CategoryToRule", foreign_key: "A", association_foreign_key: "B"
end
