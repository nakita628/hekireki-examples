class SelfHostedInstanceReport < ApplicationRecord
  self.table_name = "SelfHostedInstanceReport"

  attribute :id, default: -> { Nanoid.generate }
  attribute :unknownFields, default: 0

  validates :instanceId, presence: true
  validates :payload, exclusion: { in: [ nil ] }
end
