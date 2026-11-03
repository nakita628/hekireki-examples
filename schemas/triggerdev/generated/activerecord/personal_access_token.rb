class PersonalAccessToken < ApplicationRecord
  self.table_name = "PersonalAccessToken"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :name, presence: true
  validates :encryptedToken, exclusion: { in: [ nil ] }
  validates :obfuscatedToken, presence: true
  validates :hashedToken, presence: true, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :personal_access_tokens
  has_many :authorization_codes, foreign_key: "personalAccessTokenId", inverse_of: :personal_access_token,
                                 dependent: :destroy
end
