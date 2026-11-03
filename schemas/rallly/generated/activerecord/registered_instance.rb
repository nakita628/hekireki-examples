class RegisteredInstance < ApplicationRecord
  attribute :id, default: -> { Cuid.generate }
  alias_attribute :updated_at, :last_seen_at

  validates :instance_id, presence: true, uniqueness: true
  validates :version, presence: true
end
