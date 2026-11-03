class InstanceSettings < ApplicationRecord
  attribute :id, default: 1
  attribute :disable_user_registration, default: false
  attribute :footer_links, default: -> { JSON.parse("[]") }

  validates :instance_id, uniqueness: true
end
