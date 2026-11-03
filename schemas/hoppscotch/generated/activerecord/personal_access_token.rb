class PersonalAccessToken < ApplicationRecord
  self.table_name = "PersonalAccessToken"

  attribute :id, default: -> { Cuid.generate }
  attribute :token, default: -> { SecureRandom.uuid }
  alias_attribute :updated_at, :updatedOn

  validates :label, presence: true
  validates :token, uniqueness: true

  belongs_to :user, foreign_key: "userUid", inverse_of: :personal_access_tokens
end
