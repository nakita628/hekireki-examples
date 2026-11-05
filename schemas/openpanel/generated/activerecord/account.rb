class Account < ApplicationRecord
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :provider, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :accounts
  has_many :reset_passwords, foreign_key: "accountId", inverse_of: :account, dependent: :destroy
end
