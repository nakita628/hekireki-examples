class Account < ApplicationRecord
  self.table_name = "Account"
  self.primary_key = %w[id userId]

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  belongs_to :platform, foreign_key: "platformId", optional: true, inverse_of: :accounts
  belongs_to :user, foreign_key: "userId", inverse_of: :accounts
  has_many :activities, class_name: "Order", foreign_key: [ :accountId, :accountUserId ], inverse_of: :account,
                        dependent: :nullify
  has_many :balances, class_name: "AccountBalance", foreign_key: [ :accountId, :userId ], inverse_of: :account,
                      dependent: :destroy
  has_many :tags, class_name: "TagsOnAccounts", foreign_key: [ :accountId, :userId ], inverse_of: :account,
                  dependent: :destroy
end
