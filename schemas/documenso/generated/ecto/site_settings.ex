defmodule Documenso.SiteSettings do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          enabled: boolean(),
          data: map(),
          last_modified_at: DateTime.t(),
          last_modified_by_user: Documenso.User.t() | nil
        }

  schema "SiteSettings" do
    field(:id, :string, primary_key: true)
    field(:enabled, :boolean, default: false)
    field(:data, :map)
    field(:last_modified_at, Documenso.PrismaDateTime, autogenerate: true, source: :lastModifiedAt)
    field(:last_modified_by_user_id, :id, source: :lastModifiedByUserId)
    belongs_to(:last_modified_by_user, Documenso.User, foreign_key: :last_modified_by_user_id, define_field: false)
  end
end