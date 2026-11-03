class UserHistory < ApplicationRecord
  self.table_name = "UserHistory"

  attribute :id, default: -> { Cuid.generate }

  enum :reqType, { rest: "REST", gql: "GQL" }, validate: true

  validates :request, exclusion: { in: [ nil ] }
  validates :responseMetadata, exclusion: { in: [ nil ] }
  validates :isStarred, inclusion: { in: [ true, false ] }

  belongs_to :user, foreign_key: "userUid", inverse_of: :history
end
