class EnvelopeContent < ApplicationRecord
  self.table_name = "EnvelopeContent"

  validates :contentMeta, exclusion: { in: [ nil ] }

  belongs_to :envelope_item, foreign_key: "envelopeItemId", inverse_of: :contents
  belongs_to :envelope, foreign_key: "envelopeId", inverse_of: :contents
  belongs_to :data_content, foreign_key: "dataContentId", optional: true, inverse_of: :envelope_contents
end
