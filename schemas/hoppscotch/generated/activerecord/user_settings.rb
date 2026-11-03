class UserSettings < ApplicationRecord
  self.table_name = "UserSettings"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :updatedOn

  validates :userUid, uniqueness: true
  validates :properties, exclusion: { in: [ nil ] }

  belongs_to :user, foreign_key: "userUid", inverse_of: :settings
end
