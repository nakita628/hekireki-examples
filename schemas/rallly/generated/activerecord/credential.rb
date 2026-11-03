class Credential < ApplicationRecord
  self.inheritance_column = nil
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :scopes, default: -> { [] }

  enum :type, { oauth: "OAUTH" }, validate: true

  validates :provider, presence: true, uniqueness: { scope: %i[user_id provider_account_id] }
  validates :provider_account_id, presence: true
  validates :secret, presence: true

  belongs_to :user, inverse_of: :credentials
  has_many :calendar_connections, dependent: :destroy
  has_many :conferencing_connections, dependent: :destroy
end
