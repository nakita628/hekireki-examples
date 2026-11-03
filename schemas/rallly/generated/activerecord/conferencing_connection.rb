class ConferencingConnection < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }

  validates :provider, presence: true, uniqueness: { scope: %i[user_id provider_account_id] }
  validates :integration_id, presence: true
  validates :provider_account_id, presence: true
  validates :email, presence: true

  belongs_to :credential, inverse_of: :conferencing_connections
  belongs_to :user, inverse_of: :conferencing_connections
end
