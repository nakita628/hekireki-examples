class Account < ApplicationRecord
  self.table_name = "account"
  self.inheritance_column = nil

  attribute :id, default: -> { Cuid.generate }
  attribute :connection_id, default: ""

  validates :type, presence: true
  validates :provider, presence: true, uniqueness: { scope: %i[provider_id connection_id] }
  validates :provider_id, presence: true

  belongs_to :user, inverse_of: :accounts
end
