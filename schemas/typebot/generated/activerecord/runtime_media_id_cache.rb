class RuntimeMediaIdCache < ApplicationRecord
  self.table_name = "RuntimeMediaIdCache"

  enum :provider, { whatsapp: "WHATSAPP", dialog360: "DIALOG360" }, validate: true

  validates :url, presence: true, uniqueness: { scope: [ :publicTypebotId, :provider ] }
  validates :mediaId, presence: true

  belongs_to :public_typebot, foreign_key: "publicTypebotId", optional: true, inverse_of: :media_ids_cache
end
