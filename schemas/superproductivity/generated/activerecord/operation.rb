class Operation < ApplicationRecord
  attribute :entity_ids, default: -> { [] }
  attribute :payload_bytes, default: 0
  attribute :is_payload_encrypted, default: false

  validates :client_id, presence: true
  validates :server_seq, presence: true, uniqueness: { scope: :user_id }
  validates :action_type, presence: true
  validates :op_type, presence: true
  validates :entity_type, presence: true
  validates :payload, exclusion: { in: [ nil ] }
  validates :vector_clock, exclusion: { in: [ nil ] }
  validates :schema_version, presence: true
  validates :client_timestamp, presence: true
  validates :received_at, presence: true

  belongs_to :user, inverse_of: :operations
end
