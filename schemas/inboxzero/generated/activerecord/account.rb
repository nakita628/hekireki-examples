class Account < ApplicationRecord
  self.table_name = "Account"
  self.inheritance_column = nil
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :type, default: "oidc"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :provider, presence: true, uniqueness: { scope: :providerAccountId }
  validates :providerAccountId, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :accounts
  has_one :email_account, foreign_key: "accountId", inverse_of: :account, dependent: :destroy
end
