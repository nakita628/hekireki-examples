class BackgroundMigration < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  attribute :state, default: -> { JSON.parse("{}") }

  validates :name, presence: true, uniqueness: true
  validates :script, presence: true
  validates :args, exclusion: { in: [ nil ] }
end
