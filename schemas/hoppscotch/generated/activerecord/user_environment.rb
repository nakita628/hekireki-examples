class UserEnvironment < ApplicationRecord
  self.table_name = "UserEnvironment"

  attribute :id, default: -> { Cuid.generate }

  validates :variables, exclusion: { in: [ nil ] }
  validates :isGlobal, inclusion: { in: [ true, false ] }

  belongs_to :user, foreign_key: "userUid", inverse_of: :environments
end
