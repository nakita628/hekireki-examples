defmodule AnythingLLM.BrowserExtensionApiKeys do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          key: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: AnythingLLM.Users.t() | nil
        }

  schema "browser_extension_api_keys" do
    field(:key, :string)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :lastUpdatedAt)
  end
end