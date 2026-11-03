class AccessToken < ApplicationRecord
  self.table_name = "access_token"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_modified_time

  validates :name, presence: true
  validates :user_id, presence: true
  validates :scopes, presence: true
  validates :sign, presence: true
  validates :expired_time, presence: true
end
